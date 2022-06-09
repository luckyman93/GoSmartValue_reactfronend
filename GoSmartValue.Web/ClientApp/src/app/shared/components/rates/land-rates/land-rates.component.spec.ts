import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandRatesComponent } from './land-rates.component';

describe('LandRatesComponent', () => {
  let component: LandRatesComponent;
  let fixture: ComponentFixture<LandRatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandRatesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LandRatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
