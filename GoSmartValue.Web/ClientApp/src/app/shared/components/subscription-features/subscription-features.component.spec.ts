import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscriptionFeaturesComponent } from './subscription-features.component';

describe('SubscriptionFeaturesComponent', () => {
  let component: SubscriptionFeaturesComponent;
  let fixture: ComponentFixture<SubscriptionFeaturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubscriptionFeaturesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscriptionFeaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
