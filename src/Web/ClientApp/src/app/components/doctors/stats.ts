export interface IStats {
  "rating": number,
  "countComments": number,
  "countLikes": number,
  "countDisLikes": number
}

export class Stats implements IStats {
  countComments: number;
  countDisLikes: number;
  countLikes: number;
  rating: number;

  static deserialize(data: any): Stats {
    const stats = new Stats()

    stats.countComments = data.countComments;
    stats.countDisLikes = data.countDisLikes;
    stats.countLikes = data.countLikes;
    stats.rating = data.rating;

    return stats;
  }
}
