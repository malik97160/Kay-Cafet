import { ViewportScroller } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  constructor(private viewPortScroller : ViewportScroller) { }

  ngOnInit(): void {
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
}
