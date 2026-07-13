
import { Component, EventEmitter, Input, Output, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-view-toggle',
  imports: [],
  templateUrl: './view-toggle.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './view-toggle.component.scss'
})
export class ViewToggleComponent {
  @Input() viewMode: 'available' | 'rented' = 'available';
  @Output() modeChanged = new EventEmitter<'available' | 'rented'>();
}