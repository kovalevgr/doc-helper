export interface IInformation {
  type: number;
  title: string;
  priority: number;
}

export class Information implements IInformation {
  priority: number;
  title: string;
  type: number;

  static deserialize(data: any): Information {
    const information = new Information()

    information.priority = data.priority;
    information.title = data.title;
    information.type = data.type;

    return information;
  }
}
