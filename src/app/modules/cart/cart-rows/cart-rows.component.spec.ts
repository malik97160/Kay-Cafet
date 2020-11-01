import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartRowsComponent } from './cart-rows.component';

describe('CartRowsComponent', () => {
  let component: CartRowsComponent;
  let fixture: ComponentFixture<CartRowsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CartRowsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CartRowsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
