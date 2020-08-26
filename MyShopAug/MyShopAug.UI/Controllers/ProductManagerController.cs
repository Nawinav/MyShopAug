﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShopAug.Core;
using MyShopAug.Core.Contracts;
using MyShopAug.Core.Models;
using MyShopAug.Core.ViewModels;
using MyShopAug.DataAccess.InMemory;


namespace MyShopAug.UI.Controllers
{
    public class ProductManagerController : Controller
    {
        // GET: ProductManager
        IRepoistory<Product> context;
        IRepoistory<ProductCategory> productCategories;
        public ProductManagerController(IRepoistory<Product> context, IRepoistory<ProductCategory> productCategories)
        {
            this.context = context;
            this.productCategories = productCategories;
        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
         
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.commit();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            ProductCategory category = new ProductCategory();
        
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();

            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Name = product.Name;
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Price = product.Price;
                productToEdit.Image = product.Image;
                context.commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {

            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {

            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.commit();
                return RedirectToAction("Index");
            }

        }
    }
}