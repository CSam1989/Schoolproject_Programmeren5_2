import { Category } from "./category.enum";

export interface Product {
  id: number | null;
  name: string;
  price: number;
  category: Category;
  description: string;
  photoUrl: string;
  photoId: string;
}
