import { Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e) {
    let header = document.getElementById('headerBandeau');
    let categories = document.getElementById("categories");
    if (window.pageYOffset > (header.offsetTop + categories.offsetTop)) {
      header.classList.add('stickyHeader');
      //categories.classList.add('stickyHeader');
    } else {
     let element = document.getElementById('headerBandeau');
       element.classList.remove('stickyHeader'); 
    }
 }
}
