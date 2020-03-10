import { IValueRange } from "./ValueRange";

export interface IFood {
    name: string;
    id: number;
    parentId: number | null;
    descriptionMarkdown: string | null;
    nutrientsPer100Gram: INutrientCountPer100Gram[];
}

export interface INutrientCountPer100Gram {
    nutrientId: number;
    countInGramsPer100GramsOfFood: IValueRange;
}