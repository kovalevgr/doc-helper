using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Domain.Common.Interfaces;
using DocHelper.Domain.Entities.DoctorAggregate;
using DocHelper.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Persistence.Seeds.Doctors
{
    public class InformationsSeed : IApplicationDbSeed
    {
        public int Priority { get; } = 4;

        public void Refresh(IApplicationDbContext context)
        {
            context.Informations.ToList().ForEach(e => context.Informations.Remove(e));

            context.SaveChanges();
        }

        public async Task SeedAsync(IApplicationDbContext context)
        {
            if (!await context.Informations.AnyAsync())
            {
                var doctors = ApplicationDbSeed.GetReferences<List<Doctor>>(DoctorsSeed.GetReferenceKeyName());

                foreach (var doctor in doctors)
                {
                    var informations = new List<Information>
                    {
                        new Information(InformationType.WorkExperience,
                            "1993 г. - невролог в Цетральном военном госпитале (СГВ, Польша)", 1, doctor),
                        new Information(InformationType.WorkExperience,
                            "2013 г. - заведующая отделением специализированного приема Центральной Районной поликлиники Деснянського р-на, внештатный главный невролог района",
                            1, doctor),
                        new Information(InformationType.WorkExperience,
                            "2010 г.- невропатолог клиники «Эфферентной терапии»", 1, doctor),
                        new Information(InformationType.WorkExperience, "2016 г. - невролог клиники «ХелсиЕндХеппи»", 1,
                            doctor),

                        new Information(InformationType.Procedures, "консультативный прием", 2, doctor),
                        new Information(InformationType.Procedures, "осмотр и изучение анамнеза", 2, doctor),
                        new Information(InformationType.Procedures, "назначение и коррекция схемы лечения", 2, doctor),


                        new Information(InformationType.Education,
                            "Львовский национальный медицинский университет им Д. Галицкого - педиатрия (2002)", 3,
                            doctor),
                    };

                    foreach (var information in informations)
                    {
                        await context.Informations.AddAsync(information);
                    }
                }

                await context.SaveChangesAsync();
            }
        }
    }
}