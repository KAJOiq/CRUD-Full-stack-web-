using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mohammed_hazim.Context;
using mohammed_hazim.Model;
using mohammed_hazim.Model.DTOs;

namespace mohammed_hazim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly MainAppContext _mainAppContext;

        public CarsController(MainAppContext mainAppContext)
        {
            _mainAppContext = mainAppContext;
        }

        // get all cars
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // entryPoint of DB comuniction
            var data = await _mainAppContext.Cars.ToListAsync();
            return Ok(data);
        }

        //retrieve a single car by its id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            Cars cars = new Cars();
            var car = await _mainAppContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            cars.Id = car.Id;
            cars.OwnerName = car.OwnerName;
            cars.OwnerSurename = car.OwnerSurename;
            cars.CarPlateNum = car.CarPlateNum;
            cars.CarBrandName = car.CarBrandName;
            cars.CarModel = car.CarModel;
            cars.ChassisNumber = car.ChassisNumber;


            return Ok(cars);
        }
        //// Add new car

        //api/cars/Register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCar([FromBody] CarsDTO? carsDTO)
        {
            if (carsDTO == null)
            {
                return BadRequest("Car data is null.");
            }

            try
            {
                // تأكد من إضافة السيارة إلى قاعدة البيانات هنا
                Cars c = new Cars();
                {
                    c.OwnerName = carsDTO.OwnerName;
                    c.OwnerSurename = carsDTO.OwnerSurename;
                    c.CarBrandName = carsDTO.CarBrandName;
                    c.CarPlateNum = carsDTO.CarPlateNum;
                    c.CarModel = carsDTO.CarModel;
                    c.ChassisNumber = carsDTO.ChassisNumber;
                };

                _mainAppContext.Cars.Add(c);
                await _mainAppContext.SaveChangesAsync();

                return Ok(c); // أو return CreatedAtAction إذا كنت تريد إرجاع موقع المورد الجديد
            }
            catch (DbUpdateException dbEx)
            {
                // سجل تفاصيل الخطأ في قاعدة البيانات
                Console.WriteLine($"Database error while adding car: {dbEx.Message}");
                return StatusCode(500, new { message = "Database error while adding car", details = dbEx.InnerException?.Message });
            }
            catch (Exception ex)
            {
                // سجل الخطأ وتأكد من إرجاع رسالة واضحة
                Console.WriteLine($"Error adding car: {ex.Message}");
                return StatusCode(500, new { message = "Internal server error. Failed to add car.", details = ex.Message });
            }
            //Cars c = new Cars();
            //c.OwnerName = carsDTO.OwnerName;
            //c.OwnerSurename = carsDTO.OwnerSurename;
            //c.CarPlateNum = carsDTO.CarPlateNum;
            //c.CarBrandName = carsDTO.CarBrandName;
            //c.CarModel = carsDTO.CarModel;
            //c.ChassisNumber = carsDTO.ChassisNumber;

            //await _mainAppContext.Cars.AddAsync(c);
            //await _mainAppContext.SaveChangesAsync();

            //return Ok(c);
        }

        // Delete existing car info
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            Cars? c = await _mainAppContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (c != null)
            {
                _mainAppContext.Remove(c);
                await _mainAppContext.SaveChangesAsync();
                return Ok("Deleted");

            }

            return NotFound();
        }

        // Update existing car info
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarsDTO CarsDTO)
        {
            Cars? c = await _mainAppContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (c != null)
            {
                c.OwnerName = CarsDTO.OwnerName;
                c.OwnerSurename = CarsDTO.OwnerSurename;
                c.CarPlateNum = CarsDTO.CarPlateNum;
                c.CarBrandName = CarsDTO.CarBrandName;
                c.CarModel = CarsDTO.CarModel;
                c.ChassisNumber = CarsDTO.ChassisNumber;

                await _mainAppContext.SaveChangesAsync();
                return Ok(c);

            }
            
            return NotFound();
        }
    }
}
