import { Component, OnInit } from '@angular/core';
import {ViewChild} from '@angular/core';
import { MatTableDataSource, MatSort, MatDialog } from '@angular/material';

export interface PeriodicElement {
  composition: string;
  commandNumber: number;
  price: number;
  status: OrderStatus;
  userName: string;
  orderPickUpTime?: Date;
}

 enum OrderStatus{
  untreated,
  treated,
  toPickUp
}

const ELEMENT_DATA: PeriodicElement[] = [
  {commandNumber: 1, composition: '1 burger de Luxe + 1 Minute maid', price: 1.0079, status: OrderStatus.untreated, userName: 'Sthéphanie Libourne', orderPickUpTime: new Date()},
  {commandNumber: 2, composition: '3 sandwichs au poulets + 1 ordinaire', price: 4.0026, status: OrderStatus.untreated, userName: 'Pierre Aliko', orderPickUpTime: new Date()},
  {commandNumber: 3, composition: '1 burger de Luxe + 1 Minute maid', price: 6.941, status: OrderStatus.untreated, userName: 'Jean Dfpoir', orderPickUpTime: new Date()},
  {commandNumber: 4, composition: '1 burger de Luxe + 1 Minute maid', price: 9.0122, status: OrderStatus.untreated, userName: 'Lucas Oliviera', orderPickUpTime: new Date()},
  {commandNumber: 5, composition: '1 burger de Luxe + 1 Minute maid', price: 10.811, status: OrderStatus.untreated, userName: 'Albert Pourtali', orderPickUpTime: new Date()},
  {commandNumber: 6, composition: '1 burger de Luxe + 1 Minute maid', price: 12.0107, status: OrderStatus.treated, userName: 'Laurence Cohuil', orderPickUpTime: new Date()},
  {commandNumber: 7, composition: '1 burger de Luxe + 1 Minute maid', price: 14.0067, status: OrderStatus.treated, userName: 'Pierric Coualou', orderPickUpTime: new Date()},
  {commandNumber: 8, composition: '1 burger de Luxe + 1 Minute maid', price: 15.9994, status: OrderStatus.treated, userName: 'Samentha Menez', orderPickUpTime: new Date()},
  {commandNumber: 9, composition: '1 burger de Luxe + 1 Minute maid', price: 18.9984, status: OrderStatus.treated, userName: 'Ludivine De La riviera', orderPickUpTime: new Date()},
  {commandNumber: 10, composition: '1 burger de Luxe + 1 Minute maid', price: 20.1797, status: OrderStatus.treated, userName: 'Franck Libourne', orderPickUpTime: new Date()},
];


@Component({
  selector: 'app-dashboad',
  templateUrl: './dashboad.component.html',
  styleUrls: ['./dashboad.component.scss']
})
export class DashboadComponent implements OnInit {
  displayedColumns: string[] = ['commandNumber', 'composition', 'price', 'userName', 'pickUpDeliveryTime', 'status', 'actions'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);
  pickUpTimes: string[] = ['12:00', '12:05', '12:10', '12:30', '12:45', '13:00'];
  orderStatus = OrderStatus;
  // @ViewChild(MatSort) sort: MatSort;

  // ngAfterViewInit() {
  //   this.dataSource.sort = this.sort;
  // }
  /**
   *
   */
  // constructor(public dialogRef: MatDialog) {
  // }
  ngOnInit() {
  }

  openConfirmRefuseOrderDialog() {
    if(confirm(`Est tu sûr de vouloir refuser cette commande? Une notification sera envoyée à userName`)) {
      console.log("Implement delete functionality here");
    }
  }

}
