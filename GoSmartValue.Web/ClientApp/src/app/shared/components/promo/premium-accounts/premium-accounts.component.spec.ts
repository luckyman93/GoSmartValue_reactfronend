import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PremiumAccountsComponent } from './premium-accounts.component';

describe('PremiumAccountsComponent', () => {
  let component: PremiumAccountsComponent;
  let fixture: ComponentFixture<PremiumAccountsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PremiumAccountsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PremiumAccountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
