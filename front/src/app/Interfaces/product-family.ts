import { Product } from './product';

export interface ProductFamily {
    Id: string;
    FamilyName: string;
    Products: Product[];
}