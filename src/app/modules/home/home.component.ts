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
    if (window.pageYOffset > (header.clientHeight + categories.clientHeight)) {
      header.classList.add('stickyHeader');
      categories.classList.add('stickyMenu');
      categories.style.top = `${header.clientHeight}px`;
    } else {
       header.classList.remove('stickyHeader'); 
       categories.classList.remove('stickyMenu');
    }
 }
}
