import { ProductFamily } from 'src/app/Interfaces/product-family';
import { Product } from 'src/app/Interfaces/product';

const formuleProducts: Product[] = [
    { Id : "1",Name : "formule végétarienne", Description : "formule composée d'un sandwich végétarien ainsi que d'une boisson.", UnitPrice : 9},
    { Id : "2",Name : "formule végan", Description : "fjsqmldkfjqsoifner azfjzmlefjzhamlfihazlf zlefjzfj", UnitPrice : 7.50},
    { Id : "3",Name : "formule allégée", Description : "formule super", UnitPrice : 6.6, IsSoldOut: true},
    { Id : "4",Name : "formule bon vivant", Description : "bon vivant", UnitPrice : 5.9, Photo: "https://d359hnfgsmn1y2.cloudfront.net/upload/product_foods/media/65101/web/1084ca50eec2dca8c20c965d59c3fbed38d5b0730f4d2a786a55df9750e164ec.png"},
    { Id : "5",Name : "formule oasis + sandwich poulet + cookies", Description : "", UnitPrice : 10.5},
    { Id : "6",Name : "formule Yves Leborgne", Description : "", UnitPrice : 8}
];

const sandwichProducts: Product[] = [
    { Id : "7",Name : "sandwich au poulet", Description : "sandwich au poulet.", UnitPrice : 9},
    { Id : "8",Name : "sandwich au jambon fromage", Description : "sandwich au jambon de bayonne et au fromage de brebis", UnitPrice : 7.50},
    { Id : "9",Name : "sandwich au corned beef", Description : "", UnitPrice : 6.6},
    { Id : "10",Name : "sandwich complet", Description : "sandwich au jambon fromage et saucisses", UnitPrice : 5.9},
    { Id : "11",Name : "sandwich thon mayonnaise", Description : "", UnitPrice : 10.5},
    { Id : "12",Name : "sandwich merguez", Description : "", UnitPrice : 8}
];
const saladProducts: Product[] = [
    { Id : "13",Name : "salade végétarienne", Description : "formule composée d'une salade végétarien ainsi que d'une boisson.", UnitPrice : 9},
    { Id : "14",Name : "salade végan", Description : "fjsqmldkfjqsoifner azfjzmlefjzhamlfihazlf zlefjzfj", UnitPrice : 7.50},
    { Id : "15",Name : "salade allégée", Description : "formule super", UnitPrice : 6.6},
    { Id : "16",Name : "salade bon vivant", Description : "bon vivant", UnitPrice : 5.9},
    { Id : "17",Name : "salade oasis + sandwich poulet + cookies", Description : "", UnitPrice : 10.5},
    { Id : "18",Name : "salade Yves Leborgne", Description : "", UnitPrice : 8}
];
const drinkProducts: Product[] = [
    { Id : "19",Name : "coca cola", Description : "Boisson rafraîchissante aux extraits végétaux.", UnitPrice : 1, Photo: "assets/images/cocaCola.jpg"},
    { Id : "20",Name : "oasis tropical", Description : "fjsqmldkfjqsoifner azfjzmlefjzhamlfihazlf zlefjzfj", UnitPrice : 1},
    { Id : "21",Name : "orangina", Description : "formule super", UnitPrice : 1},
    { Id : "22",Name : "perrier", Description : "bon vivant", UnitPrice : 1},
    { Id : "23",Name : "capes", Description : "", UnitPrice : 1},
    { Id : "24",Name : "bière blonde Heineken", Description : "", UnitPrice : 1}
];
const dessertProducts: Product[] = [
    { Id : "25",Name : "panna cotta", Description : "recette d'origine italienne;", UnitPrice : 9},
    { Id : "26",Name : "flan au coco", Description : "flan au coco maison", UnitPrice : 7.50},
    { Id : "27",Name : "sundae caramel", Description : "", UnitPrice : 6.6},
    { Id : "28",Name : "fondant au chocolat", Description : "délicieux fondant au chocolat", UnitPrice : 5.9},
];
export const ProductFamilies: ProductFamily[] = [
    {Id : "29", FamilyName : "formules", Products : formuleProducts },
    {Id : "30", FamilyName : "sandwichs", Products : sandwichProducts },
    {Id : "31", FamilyName : "salades", Products : saladProducts },
    {Id : "32", FamilyName : "boissons", Products : drinkProducts },
    {Id : "33", FamilyName : "desserts", Products : dessertProducts }
];