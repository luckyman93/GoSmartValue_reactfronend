import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-upload-valuation',
  templateUrl: './upload-valuation.component.html',
  styleUrls: ['./upload-valuation.component.scss']
})
export class UploadValuationComponent implements OnInit {

  uploadedFiles: any[] = [];

  acceptedFiles: string = ".pdf, .jpg, .png, .doc, .docx, .xls, .xlsx, .csv";

  constructor(private messageService: MessageService) {}

  ngOnInit(): void {
  }
  
  onUpload(event: any) {
      for(let file of event.files) {
          this.uploadedFiles.push(file);
      }

      this.messageService.add({severity: 'info', summary: 'File Uploaded', detail: ''});
  }

}
