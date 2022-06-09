import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstantValuationComponent } from './instant-valuation.component';

describe('InstantValuationComponent', () => {
  let component: InstantValuationComponent;
  let fixture: ComponentFixture<InstantValuationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstantValuationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstantValuationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
