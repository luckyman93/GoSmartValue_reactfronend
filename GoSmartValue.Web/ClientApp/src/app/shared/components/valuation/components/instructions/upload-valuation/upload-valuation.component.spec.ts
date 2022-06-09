import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadValuationComponent } from './upload-valuation.component';

describe('UploadValuationComponent', () => {
  let component: UploadValuationComponent;
  let fixture: ComponentFixture<UploadValuationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UploadValuationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadValuationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
