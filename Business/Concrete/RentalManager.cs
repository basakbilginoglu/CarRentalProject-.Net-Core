using Business.Abstract;
using Business.Constants;
using Core.Results;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
           _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            var results = _rentalDal.GetAll(c => c.CarId == rental.CarId);
            foreach (var result in results)
            {
                if (result.ReturnDate == null || result.ReturnDate > rental.RentDate)
                {
                    return new ErrorResult(Messages.CarNotFounded);
                }
            }
           
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
           

        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {

            return new DataResult<List<Rental>>(_rentalDal.GetAll(), true, Messages.RentalListed);

        }

        public IResult Update(Rental rental)
        {

            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }
    }
}
