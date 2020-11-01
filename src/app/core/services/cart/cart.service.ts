import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Product } from 'src/app/Interfaces/product';
import { StorageService } from '../storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private _cartKey: string = 'cartProducts';
  //observable that is fired when the basket is dropped
  private basketDropedSource = new Subject();
  private basketChangedSource = new Subject<Product>();
  basketDroped$ = this.basketDropedSource.asObservable();
  basketChanged$= this.basketChangedSource.asObservable();

  constructor(private storageService: StorageService) { }

  public addProductToBasket(item: Product) {
    let products = this.getProductsFromBasket();
    if(!products.some((product) => product.Id == item.Id))
      products.push(item);
    else{
      throw new Error('products already in basket');  
    }
    this.storageService.store(this._cartKey, products);
    this.basketChangedSource.next(item);
  }
  
  public getProductsFromBasket(): Product[] {
    let products = this.storageService.retrieve(this._cartKey)
    if(!products){
      products = [];
    }
    return products;
  }

  public updateProductInBasket(item: Product){
    let products = this.getProductsFromBasketOrThrow();
    let index = this.getCurrentProductIndex(products, item);
    products[index] = item;
    this.replaceProductsInBasket(products);
    this.basketChangedSource.next(item);
  }

  private getCurrentProductIndex(products: Product[], item: Product) {
    let index = products.findIndex((product) => product.Id == item.Id);
    if (index === -1) {
      throw new Error("the product is not found in basket");
    }
    return index;
  }

  public removeProductInBasket(item: Product){
    let products = this.getProductsFromBasketOrThrow();
    let productsWithoutCurrentItem = products.filter((product) => product.Id != item.Id);
    this.replaceProductsInBasket(productsWithoutCurrentItem);
    this.basketChangedSource.next(item);
  }

  public dropBasket(){
    this.storageService.clear(this._cartKey);
    this.basketDropedSource.next();
  }

  private getProductsFromBasketOrThrow() {
    let products = this.getProductsFromBasket();
    if (products == undefined)
      throw new Error('The basket is empty');
    return products;
  }

  private replaceProductsInBasket(products: Product[]) {
    this.storageService.store(this._cartKey, products);
  }

}
