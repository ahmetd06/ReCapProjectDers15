using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [CacheAspect]
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        
        [ValidationAspect(typeof(UserValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IUserService.Get")]
        public void Add(User user)
        {
            _userDal.Add(user);
        }

        [CacheAspect]
        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
