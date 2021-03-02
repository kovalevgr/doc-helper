export interface ILocation {
  country?: string | undefined;
  city: string;
}

export class Location implements ILocation {
  city: string;
  country: string | undefined;

  static deserialize(data: any): Location {
    const location = new Location()

    location.city = data.name;

    return location;
  }
}
