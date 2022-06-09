import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingMaterialCostComponent } from './building-material-cost.component';

describe('BuildingMaterialCostComponent', () => {
  let component: BuildingMaterialCostComponent;
  let fixture: ComponentFixture<BuildingMaterialCostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BuildingMaterialCostComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BuildingMaterialCostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
