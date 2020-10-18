import { ProductFamily } from 'src/app/Interfaces/product-family';
import { Product } from 'src/app/Interfaces/product';

const formuleProducts: Product[] = [
    { Id : "1",Name : "formule végétarienne", Description : "formule composée d'un sandwich végétarien ainsi que d'une boisson.", UnitPrice : 9, Photo: "https://d359hnfgsmn1y2.cloudfront.net/upload/product_foods/media/65101/web/1084ca50eec2dca8c20c965d59c3fbed38d5b0730f4d2a786a55df9750e164ec.png"},
    { Id : "2",Name : "formule végan", Description : "fjsqmldkfjqsoifner azfjzmlefjzhamlfihazlf zlefjzfj", UnitPrice : 7.50},
    { Id : "3",Name : "formule allégée", Description : "formule super", UnitPrice : 6.6},
    { Id : "4",Name : "formule bon vivant", Description : "bon vivant", UnitPrice : 5.9},
    { Id : "5",Name : "formule oasis + sandwich poulet + cookies", Description : "", UnitPrice : 10.5},
    { Id : "6",Name : "formule Yves Leborgne", Description : "", UnitPrice : 8}
];

const sandwichProducts: Product[] = [
    { Id : "1",Name : "sandwich au poulet", Description : "sandwich au poulet.", UnitPrice : 9},
    { Id : "2",Name : "sandwich au jambon frommage", Description : "sandwich au jambon de bayonne et au fromage de brebis", UnitPrice : 7.50},
    { Id : "3",Name : "sandwich au corned beef", Description : "", UnitPrice : 6.6},
    { Id : "4",Name : "sandwich complet", Description : "sandwich au jambon fromage et saucisses", UnitPrice : 5.9},
    { Id : "5",Name : "sandwich thon mayonnaise", Description : "", UnitPrice : 10.5},
    { Id : "6",Name : "sandwich merguez", Description : "", UnitPrice : 8}
];
const saladProducts: Product[] = [
    { Id : "1",Name : "salade végétarienne", Description : "formule composée d'une salade végétarien ainsi que d'une boisson.", UnitPrice : 9},
    { Id : "2",Name : "salade végan", Description : "fjsqmldkfjqsoifner azfjzmlefjzhamlfihazlf zlefjzfj", UnitPrice : 7.50},
    { Id : "3",Name : "salade allégée", Description : "formule super", UnitPrice : 6.6},
    { Id : "4",Name : "salade bon vivant", Description : "bon vivant", UnitPrice : 5.9},
    { Id : "5",Name : "salade oasis + sandwich poulet + cookies", Description : "", UnitPrice : 10.5},
    { Id : "6",Name : "salade Yves Leborgne", Description : "", UnitPrice : 8}
];
const drinkProducts: Product[] = [
    { Id : "1",Name : "coca cola", Description : "Boisson rafraîchissante aux extraits végétaux.", UnitPrice : 1, Photo: "assets/images/cocaCola.jpg"},
    { Id : "2",Name : "oasis tropical", Description : "fjsqmldkfjqsoifner azfjzmlefjzhamlfihazlf zlefjzfj", UnitPrice : 1},
    { Id : "3",Name : "orangina", Description : "formule super", UnitPrice : 1},
    { Id : "4",Name : "perrier", Description : "bon vivant", UnitPrice : 1},
    { Id : "5",Name : "capes", Description : "", UnitPrice : 1},
    { Id : "6",Name : "bière blonde Heineken", Description : "", UnitPrice : 1}
];
const dessertProducts: Product[] = [
    { Id : "1",Name : "panna cotta", Description : "recette d'origine italienne;", UnitPrice : 9},
    { Id : "2",Name : "flan au coco", Description : "flan au coco maison", UnitPrice : 7.50},
    { Id : "3",Name : "sundae caramel", Description : "", UnitPrice : 6.6},
    { Id : "4",Name : "fondant au chocolat", Description : "délicieux fondant au chocolat", UnitPrice : 5.9},
];
export const ProductFamilies: ProductFamily[] = [
    {Id : "1", FamilyName : "formules", Products : formuleProducts },
    {Id : "2", FamilyName : "sandwichs", Products : sandwichProducts },
    {Id : "3", FamilyName : "salades", Products : saladProducts },
    {Id : "4", FamilyName : "boissons", Products : drinkProducts },
    {Id : "5", FamilyName : "desserts", Products : dessertProducts }
];