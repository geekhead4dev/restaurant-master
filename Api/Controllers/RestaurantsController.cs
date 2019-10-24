﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using ServiceLayer.DTO;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        IRestaurantService restaurantService;
        private readonly IMapper mapper;
        public RestaurantsController(IRestaurantService _restaurantService, IMapper _mapper)
        {
            restaurantService = _restaurantService;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var restaurants = restaurantService.GetAll();
            var response = new List<RestaurantResponseDTO>();
            if (restaurants == null || !restaurants.Any())
                return Ok(response);

            foreach (var rest in restaurants)
            {
                var resp = new RestaurantResponseDTO
                {
                    ID = rest.Id,
                    Name = rest.Name,
                    Address = rest.Address,
                    Description = rest.Description,
                    Latitude = rest.Latitude,
                    Longitude = rest.Longitude,
                    PhoneNumber = rest.PhoneNumber,
                    Priority = rest.Priority,
                    DateCreated = rest.DateCreated,
                    DateUpdated = rest.DateUpdated
                };

                // get images
                if(rest.RestaurantImages != null && rest.RestaurantImages.Any())
                {
                    var images = new List<ImageResponseDTO>();
                    foreach (var img in rest.RestaurantImages)
                    {
                        images.Add(
                            new ImageResponseDTO
                            {
                                ID = img.Id,
                                ImagePriority = img.ImagePriority,
                                ImageUrl = img.ImageUrl
                            });
                    }
                    resp.Images = images;
                }

                // get working hours
                if (rest.WorkingHours != null && rest.WorkingHours.Any())
                {
                    var workingImages = new List<WorkingHourResponseDTO>();
                    foreach (var hour in rest.WorkingHours)
                    {
                        workingImages.Add(
                            new WorkingHourResponseDTO
                            {
                                ID = hour.Id,
                                Day = hour.Day,
                                FromTime = hour.FromTime,
                                ToTime = hour.ToTime
                            });
                    }
                    resp.WorkingHours = workingImages;
                }

                response.Add(resp);
            }
            // var restaurantResponse = mapper.Map<List<RestaurantResponseDTO>>(restaurants);
            return Ok(response);
        }

        [HttpGet("basic-details")]
        public IActionResult GetAllRestaurantBasic()
        {
            var all = restaurantService.GetAllWithBasicDetails();
            return Ok(all);
        }

        [HttpPost]
        public IActionResult AddRestaurant([FromBody] RestaurantCreateRequestDTO restaurantRequest)
        {
            if (restaurantRequest == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            var restaurant = new Restaurant
            {
                Name = restaurantRequest.Name,
                Address = restaurantRequest.Address,
                Longitude = restaurantRequest.Longitude,
                Latitude = restaurantRequest.Latitude,
                PhoneNumber = restaurantRequest.PhoneNumber,
                Priority = restaurantRequest.Priority
            };

            // save images
            if(restaurantRequest.Images != null && restaurantRequest.Images.Any())
            {
                foreach (var image in restaurantRequest.Images)
                {
                    image.DateCreated = DateTime.Now;
                    image.DateUpdated = DateTime.Now;
                }
                restaurant.RestaurantImages = restaurantRequest.Images; 
            }

            // save the working hours
            if (restaurantRequest.WorkingHours != null && restaurantRequest.WorkingHours.Any())
            {
                foreach (var hour in restaurantRequest.WorkingHours)
                {
                    hour.DateCreated = DateTime.Now;
                    hour.DateUpdated = DateTime.Now;
                }
                restaurant.WorkingHours = restaurantRequest.WorkingHours;
            }

            // commit to db
            var response = restaurantService.CreateNewRestaurant(restaurant);

            return Ok(response);
        }

        [HttpGet("meal-categories")]
        public IActionResult GetMealCategories()
        {
            var mealCategories = restaurantService.GetMealCategories();
            return Ok(mealCategories);
        }

        [HttpGet]
        [Route("meal-types/{restaurantId}")]
        public IActionResult GetMealTypes([FromRoute] int restaurantId)
        {
            var mealTypes = restaurantService.GetAllMealTypesByRestaurantId(restaurantId);
            return Ok(mealTypes);
        }
    }
}