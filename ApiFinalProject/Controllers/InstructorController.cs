using ApiFinalProject.DTO.InstructorDTO;
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using ApiFinalProject.Services.Instructors;
using ApiFinalProject.Services.Specalazation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        

        IInstructor iinstructor;
        ISpecalazation ispecalazation;
        public InstructorController(IInstructor ins,ISpecalazation specalazation)
        {
            iinstructor = ins;
            ispecalazation = specalazation;

        }
        [HttpGet]
        public IActionResult GetAllInstuctors()
        {
            List<Instructor> instructors = iinstructor.GetAll();
            List<Specialization> specializations = ispecalazation.GetAll();

            var specializationDTOs = (from instructor in instructors
                                      join specialization in specializations
                                      on instructor.SpecializationId equals specialization.Id
                                      select new InstructorInformation
                                      {
                                          Id = instructor.Id,
                                          Name = instructor.Name,
                                          ImageUrl = instructor.ImageUrl,
                                          Address = instructor.Address,
                                          ExperienceAge = instructor.ExperienceAge,
                                          SpecializationName = specialization.Name
                                      }).ToList();

            return Ok(specializationDTOs);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetInstuctorById(int id) {
        
            Instructor ins = iinstructor.GetbyID(id);
            Specialization specialization =ispecalazation.GetbyID(ins.SpecializationId);
            InstructorInformation instructorDTO = new InstructorInformation
            {
                Id = ins.Id,
                Name = ins.Name,
                ImageUrl = ins.ImageUrl,
                Address = ins.Address,
                ExperienceAge = ins.ExperienceAge,
                SpecializationName =specialization.Name
                
            };
            return Ok(instructorDTO);

        
        }

        [HttpGet("Top5")]
        public IActionResult GetTopFive()
        {
            List<Instructor> instructors = iinstructor.GetAll();

          
           
            List<Instructor> topInstructors = instructors.OrderByDescending(i => i.ExperienceAge).Take(5).ToList();
            List<Specialization> specializations = ispecalazation.GetAll();

            var topInstructorDTOs = (from instructor in topInstructors
                                     join specialization in specializations
                                     on instructor.SpecializationId equals specialization.Id
                                     select new InstructorInformation
                                     {
                                         Id = instructor.Id,
                                         Name = instructor.Name,
                                         ImageUrl = instructor.ImageUrl,
                                         Address = instructor.Address,
                                         ExperienceAge = instructor.ExperienceAge,
                                         SpecializationName = specialization.Name
                                     }).ToList();

            return Ok(topInstructorDTOs);
        }
        
        [HttpPost]
        public IActionResult addIns(Instructor i1)
        {
            iinstructor.add(i1);
            iinstructor.Save();
            return Created();
        }
        [HttpPut("{id:int}")]
        public IActionResult updateIns(int id, Instructor i1)
        {
            Instructor pro = iinstructor.GetbyID(id);

            pro.Name = i1.Name;
            pro.ExperienceAge = i1.ExperienceAge;
            pro.Address = i1.Address;
            pro.ImageUrl = i1.ImageUrl;
            pro.SpecializationId = i1.SpecializationId;
            pro.ApplicationUserId = i1.ApplicationUserId;

            iinstructor.Save();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult deleteIns(int id)
        {
            Instructor ins = iinstructor.GetbyID(id);
            iinstructor.delete(ins);
            iinstructor.Save();
            return Created();
        }
    }
}
