export interface ISpecialty {
  title: string;
  alias: string;
  count: number | undefined;
  active: boolean;
}

export class Specialty implements ISpecialty {
  title: string;
  alias: string;
  count: number | undefined;
  active: boolean;

  static deserialize(data: any): Specialty {
    const specialty = new Specialty()

    specialty.title = data.title;
    specialty.alias = data.alias;
    specialty.active = true;

    return specialty;
  }
}
