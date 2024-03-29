﻿using FirebaseAdmin.Auth;
using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Models.Enum;
using GraphQLDemo.API.Schema.Instructor;
using GraphQLDemo.API.Schema.Student;
using GraphQLDemo.API.Schema.User;
using GraphQLDemo.API.Services.Instructors;

namespace GraphQLDemo.API.Schema.Course
{

    public class CourseType:ISearchResultType
    {
        public Guid Id { get; set; }
        //[IsProjected(false)]
        public string Name { get; set; }
        public Subject Subject { get; set; }
        //[GraphQLIgnore]
        [IsProjected(true)]
        public Guid InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader) //{ get; set; }
        {
            InstructorDTO instructorDTO  = await instructorDataLoader.LoadAsync(InstructorId,CancellationToken.None);
            return new InstructorType
            {
                Id = instructorDTO.Id,
                FirstName = instructorDTO.FirstName,
                LastName = instructorDTO.LastName,
                Salary = instructorDTO.Salary,
            };
        }
        public IEnumerable<StudentType> Students { get; set; }
        [IsProjected(true)]
        public string CreatorId { get; set; }

        public async Task<UserType> Creator([Service] UserDataLoader userDataLoader)
        {
            if (CreatorId == null || CreatorId == "")
            {
                return null;
            }

            UserRecord user =  await FirebaseAuth.DefaultInstance.GetUserAsync(CreatorId);
            return await userDataLoader.LoadAsync(CreatorId,CancellationToken.None);
        }

        //public string Description()
        //{
        //    return $"{Name} is a course of {Subject}.Instructor Of this Course " +
        //        $"is {Instructor.FirstName} {Instructor.LastName}. Top Student Of This " +
        //        $"Course {Students.OrderByDescending(x => x.GPA).FirstOrDefault()?.FirstName} " +
        //        $"{Students.OrderByDescending(x => x.GPA).FirstOrDefault()?.LastName}.";
        //}

        //public string Description()
        //{
        //    return $"{Name} is a course of {Subject}.Instructor Of this Course " +
        //        $"is {Instructor.FirstName} {Instructor.LastName}. Top Student Of This " +
        //        $"Course {Students.OrderByDescending(x => x.GPA).FirstOrDefault()?.FirstName} " +
        //        $"{Students.OrderByDescending(x => x.GPA).FirstOrDefault()?.LastName}.";
        //}
    }
}
