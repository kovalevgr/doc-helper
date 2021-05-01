import {IStats, Stats} from "./stats";
import {IInformation, Information} from "./information";
import {ISpecialty, Specialty} from "../specialty/specialty";

export interface IDoctor {
  firstName: string;
  lastName: string;
  middleName: string;
  titles: string;
  workExperience: number;
  description: string;
  photo: string;

  stats: IStats | undefined;
  informations: IInformation[] | [];
  specialties: ISpecialty[] | [];
}

export class Doctor implements IDoctor {
  id: number;
  description: string;
  firstName: string;
  informations: IInformation[] | [];
  lastName: string;
  middleName: string;
  photo: string;
  stats: IStats | undefined;
  specialties: ISpecialty[] | [];
  titles: string;
  workExperience: number;

  static deserialize(data: any): Doctor {
    const doctor = new Doctor()

    doctor.id = data.id;
    doctor.firstName = data.firstName;
    doctor.lastName = data.lastName;
    doctor.middleName = data.middleName;
    doctor.titles = data.titles;
    doctor.workExperience = data.workExperience;
    doctor.description = data.description;
    doctor.photo = data.photo;

    if (data.hasOwnProperty('stats')) {
      doctor.stats = Stats.deserialize(data.stats);
    }

    if (data.hasOwnProperty('informations') && Array.isArray(data.informations)) {
      doctor.informations = data.informations.map(i => Information.deserialize(i));
    }

    if (data.hasOwnProperty('specialties') && Array.isArray(data.specialties)) {
      doctor.specialties = data.specialties.map(i => Specialty.deserialize(i.specialty));
    }

    return doctor;
  }

  public fullName(): string {
    return `${this.lastName} ${this.firstName} ${this.middleName}`
  }
}
