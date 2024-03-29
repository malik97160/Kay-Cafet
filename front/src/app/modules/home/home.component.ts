import { ViewportScroller } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CartService } from 'src/app/core/services/cart/cart.service';
import { Product } from 'src/app/Interfaces/product';
import { ProductFamily } from 'src/app/Interfaces/product-family';
import { ProductFamilies } from './mock-product-family';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  productFamilies: ProductFamily[];
  familyNames: string[];
  currency: string;
  basketItemAddedSubscription: Subscription;

  constructor(private viewPortScroller : ViewportScroller, private cartService: CartService) { }

  ngOnInit(): void {
    this.productFamilies = ProductFamilies;
    this.familyNames = this.productFamilies.map((e) => e.FamilyName);
    this.setProductQuantityFromBasket();
    this.currency = 'EUR';

    this.basketItemAddedSubscription = this.cartService.basketChanged$.subscribe((product) => {
      if(product){
        let products = this.productFamilies.map((family) => family.Products).reduce((a, b) => a.concat(b));
        let index = products.findIndex((prd) => prd.Id == product.Id);
        products[index].Quantity = product.Quantity
      }
    });
  }

  ngOnDestroy(): void {
    if(this.basketItemAddedSubscription)
      this.basketItemAddedSubscription.unsubscribe();
  }

  private setProductQuantityFromBasket() {
    let basketProducts = this.cartService.getProductsFromBasket();
    for(let i = 0; i < basketProducts.length; i++){
      let basketProduct = basketProducts[i];
      basketLoop:
        for(let j = 0; j <this.productFamilies.length; j++){
          let family = this.productFamilies[j];
          let index = family.Products.findIndex(prd => prd.Id == basketProduct.Id);
          if (index !== -1){
            family.Products[index].Quantity = basketProduct.Quantity;
            break basketLoop;
          }
        }
    }
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e) {
    let header = document.getElementById('headerBandeau');
    let categories = document.getElementById("categories");
    if (window.pageYOffset > (header.clientHeight + categories.clientHeight)) {
      header.classList.add('stickyHeader');
      categories.classList.add('stickyMenu');
      categories.style.top = `${header.clientHeight}px`;
    } else {
       header.classList.remove('stickyHeader'); 
       categories.classList.remove('stickyMenu');
    }
 }

  scrollTo(anchorName: string){
    let headerHeight = document.getElementById('headerBandeau').clientHeight;
    let categoriesHeight = document.getElementById("categories").clientHeight; 
    let stickyHeight = headerHeight + categoriesHeight;
    let anchor = document.getElementById(anchorName);
    let a = anchor.getBoundingClientRect();
    let position = window.pageYOffset > stickyHeight ? a.top + window.scrollY - stickyHeight : a.top + window.scrollY - 2*stickyHeight; 
    this.viewPortScroller.scrollToPosition([a.left, position]);
  }

  incrementProduct(product : Product){
    if(product.Quantity == null){
      product.Quantity = 0;
    }
    product.Quantity += 1;
    this.cartService.updateProductInBasket(product);
  }

  decrementProduct(product : Product){
    product.Quantity -= 1;
    if(product.Quantity > 0)
      this.cartService.updateProductInBasket(product);
    else
      this.cartService.removeProductInBasket(product);
  }

  addProduct(product: Product){
    product.Quantity = 1;
    this.cartService.addProductToBasket(product);
  }

  dropBasket(){
    this.cartService.dropBasket();
  }
}
