using System.Collections.Generic;
using AutoMapper;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class Mapper
    {
        public static BankAccount MapAccount(BankAccountDTO accountDto)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<BankAccountDTO, BankAccount>()
                .ForMember(x => x.AccountType, s => s.MapFrom(t => t.AccountType))
                .ForMember(x => x.Owner, s => s.MapFrom(t => t.Owner))
                .ForMember(x => x.State, s => s.MapFrom(t => t.State))).CreateMapper();
            return mapper.Map<BankAccountDTO, BankAccount>(accountDto);
        }

        public static IEnumerable<BankAccount> MapAccounts(IEnumerable<BankAccountDTO> accountDtos)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<BankAccountDTO, BankAccount>()
                .ForMember(x => x.AccountType, s => s.MapFrom(t => t.AccountType))
                .ForMember(x => x.Owner, s => s.MapFrom(t => t.Owner))
                .ForMember(x => x.State, s => s.MapFrom(t => t.State))).CreateMapper();
            return mapper.Map<IEnumerable<BankAccountDTO>, IEnumerable<BankAccount>>(accountDtos);
        }

        public static BankAccountDTO MapAccountToDTO(BankAccount account)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<BankAccount, BankAccountDTO>()
                .ForMember(x => x.AccountType, s => s.MapFrom(t => t.AccountType))
                .ForMember(x => x.Owner, s => s.MapFrom(t => t.Owner))
                .ForMember(x => x.State, s => s.MapFrom(t => t.State))).CreateMapper();
            return mapper.Map<BankAccount, BankAccountDTO>(account);
        }

        public static IEnumerable<AccountType> MapTypes(IEnumerable<AccountTypeDTO> typeDtos)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<AccountTypeDTO, AccountType>()).CreateMapper();
            return mapper.Map<IEnumerable<AccountTypeDTO>, IEnumerable<AccountType>>(typeDtos);
        }
    }
}
