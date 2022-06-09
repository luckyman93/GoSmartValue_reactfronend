import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstantValuationListComponent } from './instant-valuation-list.component';

describe('InstantValuationListComponent', () => {
  let component: InstantValuationListComponent;
  let fixture: ComponentFixture<InstantValuationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstantValuationListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstantValuationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
