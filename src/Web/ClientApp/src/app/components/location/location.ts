export interface ILocation {
  country?: string | undefined;
  city: string;
}

export class Location implements ILocation {
  city: string;
  country: string | undefined;

  private static defaultCityName = 'kiev';

  static deserialize(data: any): Location {
    const location = new Location()

    location.city = data.name;

    return location;
  }

  static defaultCity(): Location {
    const location = new Location()

    location.city = this.defaultCityName;

    return location;
  }
}
