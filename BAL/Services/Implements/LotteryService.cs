using AutoMapper;
using BAL.Requests;
using BAL.Response;
using BAL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class LotteryService : ILotteryService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserLotteryRepository _lotteryRepository;
        private readonly ILotteryResultRepository _lotteryResultRepository;
        private readonly IMapper _mapper;

        public LotteryService(IAccountRepository accountRepository, IUserLotteryRepository lotteryRepository, IMapper mapper, ILotteryResultRepository lotteryResultRepository)
        {
            _accountRepository = accountRepository;
            _lotteryRepository = lotteryRepository;
            _mapper = mapper;
            _lotteryResultRepository = lotteryResultRepository;
        }

        public async Task<PlaceBetResponse> PlaceBetAsync(Guid accountId, PlaceBetRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                return new PlaceBetResponse { Message = "Account not found." };
            }

            var currentSlot = GetCurrentSlot();
            var nextSlot = GetNextSlot();

            if (currentSlot >= nextSlot)
            {
                return new PlaceBetResponse { Message = "You can only place bets for the upcoming slot." };
            }

            var userBets = await _lotteryRepository.GetUserBetsAsync(accountId);
            if (userBets.Any(bet => bet.Slot == nextSlot))
            {
                return new PlaceBetResponse { Message = "You have already placed a bet for the next slot." };
            }

            if (request.SelectedNumber < 0 || request.SelectedNumber > 9)
            {
                return new PlaceBetResponse { Message = "Selected number must be between 0 and 9." };
            }

            var userLottery = new UserLottery
            {
                AccountId = accountId,
                Slot = nextSlot,
                SelectedResult = request.SelectedNumber,
                CreatedDate = DateTime.UtcNow
            };

            await _lotteryRepository.PlaceBetAsync(userLottery);

            return new PlaceBetResponse
            {
                Message = "Bet placed successfully.",
                Slot = nextSlot
            };
        }


        public async Task<IEnumerable<GetUserBetsResponse>> GetUserBetsAsync(Guid accountId)
        {
            var userBets = await _lotteryRepository.GetUserBetsAsync(accountId);
            var lotteryResults = await _lotteryResultRepository.GetAllAsync();

            return userBets.Select(bet =>
            {
                var lotteryResult = lotteryResults.FirstOrDefault(result => result.Slot == bet.Slot);

                if (lotteryResult == null)
                {
                    return new GetUserBetsResponse
                    {
                        Slot = bet.Slot,
                        SelectedNumber = bet.SelectedResult,
                        Result = null, // No result available
                        IsWinner = false,
                        Message = $"No lottery result found for slot {bet.Slot}."
                    };
                }

                var isWinner = bet.SelectedResult == lotteryResult.Result;
                return new GetUserBetsResponse
                {
                    Slot = bet.Slot,
                    SelectedNumber = bet.SelectedResult,
                    Result = lotteryResult.Result,
                    IsWinner = isWinner,
                    Message = isWinner ? "Congratulations! You won!" : "Better luck next time!"
                };
            });
        }

        public async Task<GenerateLotteryResultResponse> GenerateLotteryResultAsync()
        {
            var currentSlot = GetCurrentSlot();

            var existingResult = await _lotteryResultRepository.GetBySlotAsync(currentSlot);
            if (existingResult != null)
            {
                return new GenerateLotteryResultResponse
                {
                    Message = "Lottery result for the current slot has already been generated.",
                    Slot = currentSlot,
                    Result = existingResult.Result
                };
            }

            var random = new Random();
            var result = random.Next(0, 10);

            var lotteryResult = new LotteryResult
            {
                Slot = currentSlot,
                Result = result,
                CreatedDate = DateTime.UtcNow
            };

            await _lotteryResultRepository.AddAsync(lotteryResult);
            _lotteryResultRepository.SaveChanges();

            return new GenerateLotteryResultResponse
            {
                Message = "Lottery result generated successfully.",
                Slot = currentSlot,
                Result = result
            };
        }

        public async Task<GetCurrentUserBetResponse> GetCurrentUserBetAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                return new GetCurrentUserBetResponse { Message = "Account not found." };
            }

            var nextSlot = GetNextSlot(); 
            var userBet = await _lotteryRepository.GetUserBetBySlotAsync(accountId, nextSlot);

            if (userBet == null)
            {
                return new GetCurrentUserBetResponse { Message = $"No bet found for the next slot at {nextSlot}." };
            }

            return new GetCurrentUserBetResponse
            {
                Slot = userBet.Slot,
                SelectedNumber = userBet.SelectedResult,
                Message = "Avaiable"
            };
        }



        private DateTime GetNextSlot()
        {
            var now = DateTime.UtcNow;
            var currentHour = now.Hour;

            var nextSlotHour = currentHour + 1;

            if (nextSlotHour > 23)
            {
                nextSlotHour = 0; 
                now = now.AddDays(1);
            }

            var nextSlotUtc = new DateTime(now.Year, now.Month, now.Day, nextSlotHour, 0, 0, DateTimeKind.Utc);

            return TimeZoneInfo.ConvertTimeFromUtc(nextSlotUtc, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        }

        private DateTime GetCurrentSlot()
        {
            var now = DateTime.UtcNow;
            var currentHour = now.Hour;

            var currentSlotUtc = new DateTime(now.Year, now.Month, now.Day, currentHour, 0, 0, DateTimeKind.Utc);

            return TimeZoneInfo.ConvertTimeFromUtc(currentSlotUtc, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        }
    }

}
