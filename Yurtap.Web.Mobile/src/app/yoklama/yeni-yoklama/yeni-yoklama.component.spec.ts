import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { YeniYoklamaComponent } from './yeni-yoklama.component';

describe('YeniYoklamaComponent', () => {
  let component: YeniYoklamaComponent;
  let fixture: ComponentFixture<YeniYoklamaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YeniYoklamaComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YeniYoklamaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
