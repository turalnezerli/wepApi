﻿using Business.Abstract;
using Business.Contants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [ValidationAspect(typeof(ProductValidator),Priority =1)]
        [CacheRemoveAspect("IProductService.Get")]
        //[ValidationAspect(typeof(ProductValidator),Priority =2)]
        public IResult Add(Product product)
        {
            //magic string 
            //Business Codess

            //Evvel bu idi sonra attribute kii yuxada yazilsdi ValidationAspect 
            // ValidationTool.Validate(new ProductValidator(), product);

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);

        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        [CacheAspect(10)]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }
        [TransactionScopeAspect]
        public IResult TransactionOperation(Product product)
        {
            _productDal.Update(product);
         //   _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);


        }

        public IResult Update(Product product)
        {
          _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
