import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeSubscriptionsComponent } from './home-subscriptions.component';

describe('HomeSubscriptionsComponent', () => {
  let component: HomeSubscriptionsComponent;
  let fixture: ComponentFixture<HomeSubscriptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeSubscriptionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeSubscriptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
