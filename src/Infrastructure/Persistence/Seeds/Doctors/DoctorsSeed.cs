using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocHelper.Domain.Common.Interfaces;
using DocHelper.Domain.Entities.DoctorAggregate;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Persistence.Seeds.Doctors
{
    public class DoctorsSeed : IApplicationDbSeed
    {
        public int Priority { get; } = 3;

        public void Refresh(IApplicationDbContext context)
        {
            context.Doctors.ToList().ForEach(e => context.Doctors.Remove(e));

            context.SaveChanges();
        }

        public async Task SeedAsync(IApplicationDbContext context)
        {
            if (!await context.Doctors.AnyAsync())
            {
                List<Doctor> references = new List<Doctor>();

                var doctors = new List<Doctor>
                {
                    new Doctor("elena-gaidai", "Елена", "Гайдай", "Владимировна", "", 33,
                        "Врач проводит консультации, диагностику и терапию заболеваний нервной системы. Елена Владимировна владеет методикой немедикаментозной тренажерной коррекции заболеваний позвоночника (осложненный и неосложненный остеохондроз позвоночника , спондилоартроз, нарушения осанки, миофасциальный болевой синдром ) , разрабатывает программу физической реабилитации пациентов на вертебрологических устройствах для лечения и профилактики заболеваний опорно-двигательного аппарата Eurospine",
                        "photo"),
                    new Doctor("olga-kusebenko", "Ольга", "Козубенко", "Геннадьевна", "Детский врач", 16,
                        "Врач-невролог ведет консультативные приемы маленьких пациентов, оказывает лечебно-диагностическую помощь. Занимается снятием и расшифровкой электроэнцефалограммы головного мозга, определяет тактику ведения пациента. Лечит неврологии раннего детского возраста, специализируется на перинатальной патологии, подростковой неврологии",
                        "photo"),
                };

                foreach (var doctor in doctors)
                {
                    await context.Doctors.AddAsync(doctor);
                    
                    references.Add(doctor);
                }

                await context.SaveChangesAsync();

                ApplicationDbSeed.SetReferences(GetReferenceKeyName(), references);
            }
        }

        public static string GetReferenceKeyName() => nameof(DoctorsSeed);
    }
}