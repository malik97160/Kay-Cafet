import { CartItem } from 'src/app/Interfaces/cart-item';
import { Product } from 'src/app/Interfaces/product'

const product1: Product = { Id : "1",Name : "formule végétarienne", Description : "formule composée d'un sandwich végétarien ainsi que d'une boisson.", UnitPrice : 9, Photo: "https://d359hnfgsmn1y2.cloudfront.net/upload/product_foods/media/65101/web/1084ca50eec2dca8c20c965d59c3fbed38d5b0730f4d2a786a55df9750e164ec.png"};
const product2: Product = { Id : "2",Name : "formule végan", Description : "fjsqmldkfjqsoifner azfjzmlefjzhamlfihazlf zlefjzfj", UnitPrice : 7.50};
const product3: Product = { Id : "3",Name : "formule allégée", Description : "formule super", UnitPrice : 6.6};
const product4: Product = { Id : "4",Name : "formule bon vivant", Description : "bon vivant", UnitPrice : 5.9};
export const CartItems : CartItem[] = [
    {Product: product1, Quantity: 5},
    {Product: product2, Quantity: 1},
    {Product: product3, Quantity: 2},
    {Product: product4, Quantity: 1},
]
