export interface INutrient {
    name: string;
    id: number;
    parentId: number | null;
    descriptionMarkdown: string | null;
    isRequired: boolean;
}