import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromoSectionComponent } from './promo-section.component';

describe('PromoSectionComponent', () => {
  let component: PromoSectionComponent;
  let fixture: ComponentFixture<PromoSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PromoSectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PromoSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
