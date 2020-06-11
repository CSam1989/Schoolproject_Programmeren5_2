import { ExclVatPipe } from "./pipes/exclVat.pipe";
import { VatPipe } from "./pipes/vat.pipe";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import {
  MatBadgeModule,
  MatButtonModule,
  MatCardModule,
  MatDividerModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatPaginatorModule,
  MatSelectModule,
  MatSortModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatDialogModule,
} from "@angular/material";
import { AutosizeModule } from "ngx-autosize";

import { ErrorInterceptorProvider } from "./interceptors/error.interceptor";
import { SummaryPipe } from "./pipes/summary.pipe";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AutosizeModule,
    MatIconModule,
    MatBadgeModule,
    MatButtonModule,
    MatToolbarModule,
    MatMenuModule,
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    MatCardModule,
    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatListModule,
    MatSelectModule,
    MatDialogModule,
    MatDividerModule,
  ],
  exports: [
    SummaryPipe,
    VatPipe,
    ExclVatPipe,
    ReactiveFormsModule,
    FormsModule,
    AutosizeModule,
    MatIconModule,
    MatBadgeModule,
    MatButtonModule,
    MatToolbarModule,
    MatMenuModule,
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    MatCardModule,
    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatListModule,
    MatSelectModule,
    MatDialogModule,
    MatDividerModule,
  ],
  providers: [
    ErrorInterceptorProvider
  ],
  declarations: [
    SummaryPipe,
    VatPipe,
    ExclVatPipe
  ]
})
export class SharedModule {
}
