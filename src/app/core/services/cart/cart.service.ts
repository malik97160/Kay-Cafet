import { Injectable } from '@angular/core';
import { Product } from 'src/app/Interfaces/product';
import { StorageService } from '../storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private _cartKey: string = 'cartProducts';
  constructor(private storageService: StorageService) { }

  public addProductToBasket(item: Product) {
    let products = this.getProductsFromBasket();
    if(!products){
      products = [];
    }
    if(!products.some((product) => product.Id == item.Id))
      products.push(item);
    else
    throw new Error('products should already be in basket');
      
    this.storageService.store(this._cartKey, products);
  }

  public getProductsFromBasket(): Product[] {
    return this.storageService.retrieve(this._cartKey)
  }

  public updateProductInBasket(item: Product){
    let products = this.getProductsFromBasket();
    let index = products.findIndex((product) => product.Id == item.Id);
    products[index] = item;
    this.replaceProductsInBasket(products);
  }
  replaceProductsInBasket(products: Product[]) {
    this.storageService.store(this._cartKey, products);
  }

}
