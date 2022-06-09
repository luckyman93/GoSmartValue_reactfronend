import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IssueInstructionComponent } from './issue-instruction.component';

describe('IssueInstructionComponent', () => {
  let component: IssueInstructionComponent;
  let fixture: ComponentFixture<IssueInstructionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IssueInstructionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IssueInstructionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
