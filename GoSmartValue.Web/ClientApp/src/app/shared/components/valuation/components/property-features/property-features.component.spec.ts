import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyFeaturesComponent } from './property-features.component';

describe('PropertyFeaturesComponent', () => {
  let component: PropertyFeaturesComponent;
  let fixture: ComponentFixture<PropertyFeaturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PropertyFeaturesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyFeaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
