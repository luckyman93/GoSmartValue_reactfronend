import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingInstructionsComponent } from './pending-instructions.component';

describe('PendingInstructionsComponent', () => {
  let component: PendingInstructionsComponent;
  let fixture: ComponentFixture<PendingInstructionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PendingInstructionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PendingInstructionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
