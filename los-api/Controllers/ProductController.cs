using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using los_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace los_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private WebApiDbContext _db;

        public ProductController(WebApiDbContext db)
        {
            _db = db;
        }



        [Route("GetProductAll")]
        [HttpGet]
        public List<Product> GetProductAll()
        {
            var ProductList = (from p in _db.Product
                      select p).ToList();

            return ProductList ;
        }

        [Route("GetProductID/{name}")]
        [HttpGet]
        public List<Product> GetProductAll(string name)
        {
            var ProductList = (from p in _db.Product
                               where p.name == name
                               select p).ToList();

            return ProductList;
        }


        [Route("AddProduct")]
        [HttpPost]
        public string AddProduct([FromBody] Product data)
        {
            try
            {
                if (data != null)
                {
                    var pd = (from p in _db.Product
                              where p.name == data.name
                              select p).ToList();
                    if(pd.Count == 0)
                    {
                        _db.Product.Add(data);
                        _db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "Data Duplicate";
                    }
                }
                else
                {
                    return "Data Invalid";
                }
            }
            catch (Exception ex)
            {
                               
                return "Error " + ex.Message;
            }
        }


        [Route("UpdateProduct")]
        [HttpPost]
        public string UpdateProduct([FromBody] Product data)
        {
            try
            {
                if (data != null)
                {

                    var pd = (from p in _db.Product
                              where p.Id == data.Id
                              select p).FirstOrDefault();
                    if (pd != null)
                    {
                        pd.name = data.name;
                        pd.imageUrl = data.imageUrl;
                        pd.price = data.price;
                        _db.Update(pd);
                        _db.SaveChangesAsync();

                        return "Success";
                    }
                    else
                    {
                        return "Not found";
                    }
                }
                else
                {
                    return "Data Invalid";
                }
            }
            catch (Exception ex)
            {

                return "Error " + ex.Message;
            }
        }



        [Route("DeleteProduct")]
        [HttpPost]
        public string DeleteProduct([FromBody] Product data)
        {
            try
            {
                if (data != null)
                {

                    var pd = (from p in _db.Product
                              where p.Id == data.Id
                              select p).FirstOrDefault();
                    if (pd != null)
                    {                        
                        _db.Remove(pd);
                        _db.SaveChangesAsync();

                        return "Success";
                    }
                    else
                    {
                        return "Not found";
                    }
                }
                else
                {
                    return "Data Invalid";
                }
            }
            catch (Exception ex)
            {

                return "Error " + ex.Message;
            }
        }





        [Route("AddStock")]
        [HttpPost]
        public string AddStock([FromBody] Stock data)
        {
            try
            {
                if (data != null)
                {
                    var pd = (from p in _db.Product
                              where p.Id == data.productId
                              select p).FirstOrDefault();
                    if (pd != null)
                    {
                        var st = (from p in _db.Stock
                                  where p.productId == data.productId
                                  select p).FirstOrDefault();
                        if(st == null)
                        {
                            _db.Stock.Add(data);
                            _db.SaveChanges();
                            return "Success";
                        }
                        else
                        {
                            return "Data Duplicate";
                        }
                    }
                    else
                    {
                        return "ProductId Invalid";
                    }
                }
                else
                {
                    return "Data Invalid";
                }
            }
            catch (Exception ex)
            {

                return "Error " + ex.Message;
            }
        }


        [Route("UpdateStock")]
        [HttpPost]
        public string UpdateStock([FromBody] Stock data)
        {
            try
            {
                if (data != null)
                {   
                    var pd = (from p in _db.Product
                              where p.Id == data.productId
                              select p).FirstOrDefault();
                    if (pd != null)
                    {
                        var st = (from p in _db.Stock
                                  where p.id == data.id
                                  select p).FirstOrDefault();
                        if (st != null)
                        {
                            st.productId = data.productId;
                            st.amount = data.amount;                                
                            _db.Update(st);
                            _db.SaveChanges();
                            return "Success";
                        }
                        else
                        {
                            return "Data Duplicate";
                        }
                    }
                    else
                    {
                        return "ProductId Invalid";
                    }
                }
                else
                {
                    return "Data Invalid";
                }
            }
            catch (Exception ex)
            {

                return "Error " + ex.Message;
            }
        }


        [Route("DeleteStock")]
        [HttpPost]
        public string DeleteStock([FromBody] Stock data)
        {
            try
            {
                if (data != null)
                {
                    var pd = (from p in _db.Stock
                              where p.id == data.id
                              select p).FirstOrDefault();
                    if (pd != null)
                    {
                        _db.Remove(pd);
                        _db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "Not found";
                    }
                }
                else
                {
                    return "Data Invalid";
                }
            }
            catch (Exception ex)
            {

                return "Error " + ex.Message;
            }
        }




    }
}
