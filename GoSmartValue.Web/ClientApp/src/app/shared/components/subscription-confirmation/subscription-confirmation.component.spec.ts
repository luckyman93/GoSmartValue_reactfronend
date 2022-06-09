import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscriptionConfirmationComponent } from './subscription-confirmation.component';

describe('SubscriptionConfirmationComponent', () => {
  let component: SubscriptionConfirmationComponent;
  let fixture: ComponentFixture<SubscriptionConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubscriptionConfirmationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscriptionConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
