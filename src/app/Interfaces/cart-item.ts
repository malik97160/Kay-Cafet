import { Product } from './product';

export interface CartItem {
    Product: Product,
    Quantity: number
}