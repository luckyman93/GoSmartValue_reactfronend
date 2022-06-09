import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewInstructionsComponent } from './new-instructions.component';

describe('NewInstructionsComponent', () => {
  let component: NewInstructionsComponent;
  let fixture: ComponentFixture<NewInstructionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewInstructionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewInstructionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
