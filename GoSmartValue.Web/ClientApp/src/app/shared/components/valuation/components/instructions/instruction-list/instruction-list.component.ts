import { Component, Input, OnInit } from '@angular/core';
import { DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';
import { UploadValuationComponent } from '../upload-valuation/upload-valuation.component';

@Component({
  selector: 'app-instruction-list',
  templateUrl: './instruction-list.component.html',
  styleUrls: ['./instruction-list.component.scss']
})
export class InstructionListComponent implements OnInit {

  @Input() instructions: any[] = [];
  @Input() columns: any[] = [];
  @Input() valuer: boolean= false;

  ref: DynamicDialogRef = new DynamicDialogRef();
  
  
  constructor(private dialogService: DialogService) { }

  ngOnInit(): void {
    
  }

  uploadValuation(instruction: any)
  {
    this.ref = this.dialogService.open(UploadValuationComponent, {
      header: 'Submit Valuation For Plot: '+instruction.PlotNumber,
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10000,
      data: {instruction}
    });
  }

}
