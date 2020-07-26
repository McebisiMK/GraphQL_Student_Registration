using GraphQL.Types;
using Registration.Entities.Models;
using Registration.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.API.GraphQL.Types
{
    public class StudentType : ObjectGraphType<Student>
    {
        public StudentType(IAddressService addressService, ISubjectService subjectService)
        {
            Field(s => s.StudentNumber);
            Field(s => s.Name);
            Field(s => s.Surname);
            Field(s => s.AddressId);
            Field(s => s.Cellphone);
            Field(s => s.IdNumber);
            Field(s => s.CourseId);
            FieldAsync<AddressType>
                (
                    "Address",
                    resolve: async ctx => await addressService.GetById(ctx.Source.AddressId)
                );
            FieldAsync<ListGraphType<SubjectType>>
                (
                    "Subjects",
                    resolve: async ctx => await subjectService.GetByCourse(ctx.Source.CourseId)
                );
        }
    }
}
