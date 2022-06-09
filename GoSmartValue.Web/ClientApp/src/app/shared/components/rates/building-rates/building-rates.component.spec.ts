import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingRatesComponent } from './building-rates.component';

describe('BuildingRatesComponent', () => {
  let component: BuildingRatesComponent;
  let fixture: ComponentFixture<BuildingRatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BuildingRatesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BuildingRatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
