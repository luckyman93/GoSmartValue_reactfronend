import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResidentialValuationComponent } from './residential-valuation.component';

describe('ResidentialValuationComponent', () => {
  let component: ResidentialValuationComponent;
  let fixture: ComponentFixture<ResidentialValuationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResidentialValuationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResidentialValuationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
